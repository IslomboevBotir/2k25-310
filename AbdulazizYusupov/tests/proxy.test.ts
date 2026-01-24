import { SecurityProxy } from '../src/core/proxy/SecurityProxy';

describe('Proxy Pattern - SecurityProxy', () => {
  test('should control access based on user role', () => {
    const viewerProxy = new SecurityProxy('viewer');
    const adminProxy = new SecurityProxy('admin');
    
    viewerProxy.activateCameras();
    expect(viewerProxy.areCamerasActive()).toBe(false);
    
    adminProxy.activateCameras();
    expect(adminProxy.areCamerasActive()).toBe(true);
  });

  test('should allow role changes', () => {
    const proxy = new SecurityProxy('viewer');
    expect(proxy.getUserRole()).toBe('viewer');
    
    proxy.setUserRole('admin');
    expect(proxy.getUserRole()).toBe('admin');
  });

  test('should restrict critical operations to admin only', () => {
    const operatorProxy = new SecurityProxy('operator');
    const adminProxy = new SecurityProxy('admin');
    
    operatorProxy.activateAlarms();
    expect(operatorProxy.areAlarmsActive()).toBe(false);
    
    adminProxy.activateAlarms();
    expect(adminProxy.areAlarmsActive()).toBe(true);
  });
});
