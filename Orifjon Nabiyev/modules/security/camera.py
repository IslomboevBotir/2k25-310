"""
modules/security/camera.py

Camera and camera decorators for the security subsystem.

Patterns used:
- Composite: CameraGroup (in security_controller) treats Camera and nested groups uniformly.
- Decorator: CameraDecorator allows adding capabilities (NightVisionDecorator, AnalyticsDecorator)
  to Camera objects without changing the Camera implementation.

This module defines:
- Camera: a concrete video sensor with basic operations.
- CameraDecorator: base decorator to add features like night vision and analytics.
- NightVisionDecorator: adds a simple night-vision toggle and enhanced low-light detection.
- AnalyticsDecorator: simulates basic analytics (motion detection count, intrusion alerts).
"""

from __future__ import annotations
from abc import ABC, abstractmethod
from typing import Dict, Any
import uuid
import random
import time


class CameraComponent(ABC):
    """Abstract component used by composite and decorators."""

    @abstractmethod
    def start(self) -> None:
        pass

    @abstractmethod
    def stop(self) -> None:
        pass

    @abstractmethod
    def snapshot(self) -> Dict[str, Any]:
        """Return a snapshot/status dict representing a frame or camera health."""
        pass

    @abstractmethod
    def status(self) -> Dict[str, Any]:
        pass


class Camera(CameraComponent):
    """
    Concrete Camera (leaf) representing a physical camera.

    Attributes:
      - identifier: unique id
      - location: human readable location string
      - recording: whether actively recording
      - last_frame_ts: timestamp of last snapshot
    """

    def __init__(self, location: str, identifier: str | None = None, resolution: str = "1080p"):
        self.identifier = identifier or f"cam-{uuid.uuid4().hex[:8]}"
        self.location = location
        self.resolution = resolution
        self.recording = False
        self._last_frame_ts = None
        self._uptime_secs = 0.0
        self._created_at = time.time()

    def start(self) -> None:
        if not self.recording:
            self.recording = True
            print(f"[Camera:{self.identifier}] Started recording at {self.location} ({self.resolution}).")

    def stop(self) -> None:
        if self.recording:
            self.recording = False
            print(f"[Camera:{self.identifier}] Stopped recording.")

    def snapshot(self) -> Dict[str, Any]:
        """Simulate taking a snapshot/frame and return metadata."""
        self._last_frame_ts = time.time()
        # Simulated motion score
        motion_score = round(random.random(), 2)
        frame = {
            "camera_id": self.identifier,
            "location": self.location,
            "ts": self._last_frame_ts,
            "motion_score": motion_score,
            "resolution": self.resolution,
        }
        return frame

    def status(self) -> Dict[str, Any]:
        uptime = time.time() - self._created_at
        return {
            "id": self.identifier,
            "location": self.location,
            "recording": self.recording,
            "resolution": self.resolution,
            "last_frame_ts": self._last_frame_ts,
            "uptime_secs": round(uptime, 1),
        }


class CameraDecorator(CameraComponent):
    """Base decorator that wraps a CameraComponent to extend behavior."""

    def __init__(self, wrapped: CameraComponent):
        self._wrapped = wrapped

    def start(self) -> None:
        return self._wrapped.start()

    def stop(self) -> None:
        return self._wrapped.stop()

    def snapshot(self) -> Dict[str, Any]:
        return self._wrapped.snapshot()

    def status(self) -> Dict[str, Any]:
        s = self._wrapped.status()
        s.setdefault("decorators", [])
        s["decorators"].append(self.__class__.__name__)
        return s


class NightVisionDecorator(CameraDecorator):
    """
    Adds basic night-vision capability and low-light handling.
    """

    def __init__(self, wrapped: CameraComponent, enabled: bool = True, gain: float = 1.5):
        super().__init__(wrapped)
        self.enabled = bool(enabled)
        self.gain = float(gain)

    def snapshot(self) -> Dict[str, Any]:
        frame = self._wrapped.snapshot()
        # When night vision enabled, amplify detected motion in low-light simulation
        if self.enabled:
            # Simulate darker environment by randomly choosing low-light flag
            low_light = random.choice([True, False, False])  # bias towards non-low-light
            frame["low_light"] = low_light
            if low_light:
                frame["motion_score"] = min(1.0, round(frame["motion_score"] * self.gain, 2))
                frame["night_vision_applied"] = True
        return frame

    def status(self) -> Dict[str, Any]:
        s = super().status()
        s["night_vision"] = {"enabled": self.enabled, "gain": self.gain}
        return s


class AnalyticsDecorator(CameraDecorator):
    """
    Adds simple analytics counters and intrusion heuristics.

    This decorator demonstrates adding responsibilities to Camera at runtime
    without modifying the Camera class itself.
    """

    def __init__(self, wrapped: CameraComponent, motion_threshold: float = 0.6):
        super().__init__(wrapped)
        self.motion_threshold = float(motion_threshold)
        self.motion_events = 0
        self.intrusion_alerts = 0

    def snapshot(self) -> Dict[str, Any]:
        frame = self._wrapped.snapshot()
        motion = float(frame.get("motion_score", 0.0))
        if motion >= self.motion_threshold:
            self.motion_events += 1
            # Heuristic: if multiple strong motions occur quickly, count as intrusion
            if motion > 0.9:
                self.intrusion_alerts += 1
                frame["intrusion"] = True
            frame["motion_flagged"] = True
        return frame

    def status(self) -> Dict[str, Any]:
        s = super().status()
        s["analytics"] = {
            "motion_threshold": self.motion_threshold,
            "motion_events": self.motion_events,
            "intrusion_alerts": self.intrusion_alerts,
        }
        return s


# Demo when run directly
if __name__ == "__main__":
    print("Demo: Camera with decorators")
    cam = Camera(location="Central Park", identifier="CAM-CP-01")
    nv = NightVisionDecorator(cam, enabled=True, gain=1.8)
    analytics_cam = AnalyticsDecorator(nv, motion_threshold=0.5)

    analytics_cam.start()
    for _ in range(3):
        frame = analytics_cam.snapshot()
        print("Frame:", frame)
    print("Status:", analytics_cam.status())
    analytics_cam.stop()