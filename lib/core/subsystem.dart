/// Common Subsystem interface
/// This simple interface is used so Proxy/Facade can work with any subsystem uniformly.
abstract class Subsystem {
  void start();
  void stop();
  String status();
}
