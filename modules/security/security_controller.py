"""
modules/security/security_controller.py

SecurityController and CameraGroup (Composite) for the security subsystem.

Patterns used:
- Composite: CameraGroup composes CameraComponent instances (Camera or decorated cameras),
  allowing the controller to treat groups and individual cameras uniformly.
- Facade: SecurityController provides a simplified interface (arm, disarm, start_surveillance, status)
  to orchestrate cameras and alerting/recording functionality.

The controller is lightweight and intended to integrate with the central CityController.
"""

from __future__ import annotations
from typing import List, Dict, Any, Optional
from camera import CameraComponent
import threading
import time


class CameraGroup(CameraComponent):
    """
    Composite container for CameraComponent objects (cameras and nested groups).
    """

    def __init__(self, name: str):
        self.name = name
        self._members: List[CameraComponent] = []

    def add(self, comp: CameraComponent) -> None:
        self._members.append(comp)

    def remove(self, comp: CameraComponent) -> None:
        self._members = [m for m in self._members if m is not comp]

    def start(self) -> None:
        print(f"[CameraGroup:{self.name}] Starting {len(self._members)} members.")
        for m in self._members:
            m.start()

    def stop(self) -> None:
        print(f"[CameraGroup:{self.name}] Stopping {len(self._members)} members.")
        for m in self._members:
            m.stop()

    def snapshot(self) -> Dict[str, Any]:
        # Aggregate snapshots from members (first N for demo)
        snaps = []
        for m in self._members:
            try:
                snaps.append(m.snapshot())
            except Exception:
                continue
        return {"group": self.name, "snapshots": snaps}

    def status(self) -> Dict[str, Any]:
        return {
            "group": self.name,
            "count": len(self._members),
            "members": [m.status() for m in self._members],
        }

    def get_members(self) -> List[CameraComponent]:
        return list(self._members)


class SecurityController:
    """
    High-level security controller acting as a Facade.

    Responsibilities:
      - Manage a set of camera groups and standalone cameras.
      - Arm/disarm the security system (affects behavior like alerting).
      - Start/stop surveillance and perform periodic sampling (simulated).
      - Provide status aggregation for external reporters (CityController).
    """

    def __init__(self, name: str = "SecurityController", sample_interval_secs: int = 5):
        self.name = name
        self._groups: Dict[str, CameraGroup] = {}
        self._armed = False
        self._recording = False
        self._lock = threading.RLock()
        self._sample_interval_secs = int(sample_interval_secs)
        self._sampler_thread: Optional[threading.Thread] = None
        self._stop_event = threading.Event()

    def add_group(self, group: CameraGroup) -> None:
        with self._lock:
            self._groups[group.name] = group
            print(f"[SecurityController:{self.name}] Added group '{group.name}'.")

    def remove_group(self, name: str) -> None:
        with self._lock:
            if name in self._groups:
                del self._groups[name]
                print(f"[SecurityController:{self.name}] Removed group '{name}'.")

    def arm(self) -> None:
        with self._lock:
            if self._armed:
                print(f"[SecurityController:{self.name}] Already armed.")
                return
            self._armed = True
            print(f"[SecurityController:{self.name}] System armed. Starting surveillance.")
            self.start_surveillance()

    def disarm(self) -> None:
        with self._lock:
            if not self._armed:
                print(f"[SecurityController:{self.name}] Already disarmed.")
                return
            self._armed = False
            print(f"[SecurityController:{self.name}] System disarmed. Stopping surveillance.")
            self.stop_surveillance()

    def start_surveillance(self) -> None:
        with self._lock:
            if self._recording:
                print(f"[SecurityController:{self.name}] Surveillance already running.")
                return
            # Start all groups/cameras
            for g in self._groups.values():
                g.start()
            self._recording = True
            # Start sampler thread to simulate periodic analytic sampling
            self._stop_event.clear()
            self._sampler_thread = threading.Thread(target=self._sampler_loop, daemon=True)
            self._sampler_thread.start()
            print(f"[SecurityController:{self.name}] Surveillance started (sampling every {self._sample_interval_secs}s).")

    def stop_surveillance(self) -> None:
        with self._lock:
            if not self._recording:
                print(f"[SecurityController:{self.name}] Surveillance not running.")
                return
            # Stop sampler
            self._stop_event.set()
            if self._sampler_thread:
                self._sampler_thread.join(timeout=1.0)
            # Stop all groups/cameras
            for g in self._groups.values():
                g.stop()
            self._recording = False
            print(f"[SecurityController:{self.name}] Surveillance stopped.")

    def _sampler_loop(self) -> None:
        """Worker thread that periodically samples snapshots and prints analytics summary (simulated)."""
        while not self._stop_event.is_set():
            with self._lock:
                aggregated = []
                for g in self._groups.values():
                    try:
                        snap = g.snapshot()
                        # Inspect snapshots for motion flags or intrusions (best-effort)
                        for f in snap.get("snapshots", []):
                            if f.get("intrusion") or f.get("motion_flagged"):
                                aggregated.append({"camera": f.get("camera_id"), "motion": f.get("motion_score"), "intrusion": f.get("intrusion", False)})
                    except Exception:
                        continue
                if aggregated:
                    print(f"[SecurityController:{self.name}] ALERTS detected: {aggregated}")
                else:
                    print(f"[SecurityController:{self.name}] Sampled: no notable events.")
            # Sleep until next sample or until asked to stop
            self._stop_event.wait(self._sample_interval_secs)

    def status(self) -> Dict[str, Any]:
        with self._lock:
            return {
                "name": self.name,
                "armed": self._armed,
                "recording": self._recording,
                "groups": {name: g.status() for name, g in self._groups.items()},
                "sample_interval_secs": self._sample_interval_secs,
            }


# Demo when run directly
if __name__ == "__main__":
    from camera import Camera, NightVisionDecorator, AnalyticsDecorator

    print("Demo: SecurityController with CameraGroup and decorators")
    cam1 = Camera(location="Main Gate", identifier="SEC-01")
    cam2 = Camera(location="Parking Lot", identifier="SEC-02")
    cam3 = Camera(location="Storage", identifier="SEC-03")

    cam2nv = NightVisionDecorator(cam2, enabled=True, gain=1.6)
    cam3a = AnalyticsDecorator(cam3, motion_threshold=0.5)

    group_main = CameraGroup("Perimeter")
    group_main.add(cam1)
    group_main.add(cam2nv)

    group_inner = CameraGroup("Inner")
    group_inner.add(cam3a)

    controller = SecurityController()
    controller.add_group(group_main)
    controller.add_group(group_inner)

    controller.arm()
    # Let sampler run a couple of cycles for demo, then disarm
    time.sleep(2 * controller._sample_interval_secs + 1)
    controller.disarm()

    print("Final status:", controller.status())