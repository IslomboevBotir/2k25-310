"""
core/proxy/access_proxy.py

Proxy pattern implementation for subsystem access control and lazy initialization.

Purpose:
- Proxy: AccessProxy controls access to a real subsystem (the Subject). It can:
    - Enforce role-based permissions before invoking subsystem actions.
    - Lazily instantiate the real subsystem only when first needed (saving resources).
    - Log or audit calls to the subsystem (simple print-based audit in this educational example).
- This proxy is useful in SmartCity where certain actions (e.g., disarming security, restarting transport)
  must be performed only by authorized roles (admin/ops) and where creating heavy subsystem objects
  may be deferred until actually required.

Usage:
    def subject_factory(): return RealSecuritySubsystem()
    proxy = AccessProxy(subject_factory, allowed_roles=("admin", "operator"), user_role_provider=lambda: "guest")
    proxy.arm()  # will raise PermissionError for "guest"
    # if allowed, the real subject will be created lazily and the call forwarded
"""

from typing import Any, Callable, Iterable, Optional
import functools


class AccessProxy:
    """
    AccessProxy wraps creation and access to a real subsystem.

    Constructor arguments:
    - subject_factory: callable that returns the real subsystem instance when called.
    - allowed_roles: iterable of roles (strings) allowed to call proxied methods.
      If empty/None, no role checks are performed (open proxy).
    - user_role_provider: optional callable returning the current user's role (string).
      If not provided, a default provider that returns "unknown" is used.
    - name: optional name used in logs for easier debugging.
    """

    def __init__(
        self,
        subject_factory: Callable[[], Any],
        allowed_roles: Optional[Iterable[str]] = None,
        user_role_provider: Optional[Callable[[], str]] = None,
        name: Optional[str] = None,
    ):
        if not callable(subject_factory):
            raise ValueError("subject_factory must be callable and return the real subject")
        self._subject_factory = subject_factory
        self._subject: Optional[Any] = None
        self._allowed_roles = set(allowed_roles) if allowed_roles else None
        self._user_role_provider = user_role_provider or (lambda: "unknown")
        self._name = name or subject_factory.__name__

    @property
    def is_initialized(self) -> bool:
        """Returns True if the real subject has already been created."""
        return self._subject is not None

    def _ensure_subject(self):
        """Create the real subject lazily if not already created."""
        if self._subject is None:
            print(f"[AccessProxy:{self._name}] Lazy-instantiating real subject...")
            self._subject = self._subject_factory()

    def _check_permission(self, action: str):
        """
        Check whether the current user role is allowed to perform 'action'.
        Raises PermissionError if not authorized.
        """
        if self._allowed_roles is None:
            # No role checks configured
            return
        role = self._user_role_provider() or "unknown"
        if role not in self._allowed_roles:
            # Simple audit log
            print(f"[AccessProxy:{self._name}] PERMISSION DENIED: role='{role}' attempted '{action}'")
            raise PermissionError(f"Role '{role}' is not authorized to perform '{action}'")
        # Audit allowed action
        print(f"[AccessProxy:{self._name}] Authorized: role='{role}' performing '{action}'")

    def __getattr__(self, attr: str) -> Any:
        """
        Intercept attribute access to provide:
         - Permission checks for callables (methods).
         - Lazy instantiation before delegation.
        Non-callable attributes are fetched after lazy initialization without permission checks.
        """
        # Provide access to proxy internals via normal attribute access
        if attr.startswith("_"):
            raise AttributeError(attr)

        def make_wrapper(name: str):
            @functools.wraps(getattr(self._get_target(), name))
            def wrapper(*args, **kwargs):
                # Check permission for method invocation
                self._check_permission(name)
                target = self._get_target()
                method = getattr(target, name)
                return method(*args, **kwargs)
            return wrapper

        target = self._get_target(check_only=True)
        # If the attribute exists on the target and is callable, return a wrapped callable.
        if target is not None and hasattr(target, attr):
            candidate = getattr(target, attr)
            if callable(candidate):
                return make_wrapper(attr)
            else:
                # Non-callable attribute: perform lazy init and return
                self._ensure_subject()
                return getattr(self._subject, attr)

        # If target not yet created, we still need to decide: assume methods will be present and wrap to lazy-init + check
        # Return a wrapper that will instantiate and forward (useful when subject not created yet)
        def late_wrapper(*args, **kwargs):
            self._check_permission(attr)
            self._ensure_subject()
            target_attr = getattr(self._subject, attr)
            if callable(target_attr):
                return target_attr(*args, **kwargs)
            return target_attr

        return late_wrapper

    def _get_target(self, check_only: bool = False) -> Optional[Any]:
        """
        Return the current subject if instantiated, otherwise None (when check_only=True).
        When check_only=False, ensure subject is created.
        """
        if self._subject is None:
            if check_only:
                return None
            self._ensure_subject()
        return self._subject

    def get_subject(self) -> Any:
        """Force creation and return the underlying real subsystem (useful for trusted code)."""
        self._ensure_subject()
        return self._subject


# --- Demonstration / Self-test when run directly --- #
if __name__ == "__main__":
    # Define a simple Security subsystem to demonstrate proxy behavior
    class RealSecurity:
        def __init__(self):
            self.armed = False

        def arm(self):
            self.armed = True
            print("[RealSecurity] System armed.")

        def disarm(self):
            self.armed = False
            print("[RealSecurity] System disarmed.")

        def status(self):
            return {"armed": self.armed}

    # Simulate two different user role providers
    current_role = {"role": "guest"}

    def role_provider():
        return current_role["role"]

    # Factory for real security
    def security_factory():
        print("[factory] creating RealSecurity instance")
        return RealSecurity()

    # Create a proxy that only allows 'admin' and 'operator' to perform actions
    proxy = AccessProxy(security_factory, allowed_roles=("admin", "operator"), user_role_provider=role_provider, name="security")

    print("Is initialized:", proxy.is_initialized)
    try:
        print("Guest trying to arm:")
        proxy.arm()  # should raise PermissionError
    except PermissionError as e:
        print("Caught:", e)

    # Elevate role and retry
    current_role["role"] = "admin"
    print("\nAdmin trying to arm:")
    proxy.arm()  # should lazily instantiate and arm

    print("Status:", proxy.status())
    print("Is initialized:", proxy.is_initialized)