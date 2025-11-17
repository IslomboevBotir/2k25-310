import '../../modules/security/security.dart';

class SecurityProxy {
  final Security _security = Security();
  bool isAdmin = false;

  void activateCamera() {
    if (isAdmin) {
      _security.activateCameras();
    } else {
      print('UR NOT AN ADMIN');
    }
  }

  void deactivateCameras() {
    if (isAdmin) {
      _security.deactivateCameras();
    } else {
      print('UR NOT AN ADMIN');
    }
  }
}
