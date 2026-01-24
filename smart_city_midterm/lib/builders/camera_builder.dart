class SecurityCamera {
  String? resolution;
  bool nightVision = false;
  bool motionSensor = false;

  @override
  String toString() => 'Camera(res=\$resolution, night=\$nightVision, motion=\$motionSensor)';
}

class CameraBuilder {
  final SecurityCamera _cam = SecurityCamera();

  CameraBuilder setResolution(String res) {
    _cam.resolution = res;
    return this;
  }

  CameraBuilder enableNightVision() {
    _cam.nightVision = true;
    return this;
  }

  CameraBuilder enableMotionSensor() {
    _cam.motionSensor = true;
    return this;
  }

  SecurityCamera build() => _cam;
}