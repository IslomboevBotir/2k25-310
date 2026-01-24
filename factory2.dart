void main() {
  var notification = NotificationFactory.createNotification('email');
  notification.send();

  var notification2 = NotificationFactory.createNotification('sms');
  notification2.send();

  var notification3 = NotificationFactory.createNotification('push');
  notification3.send();
}

abstract class Notification {
  void send();
}

class EmailNotification implements Notification {
  @override
  void send() {
    print("Email sent notification");
  }
}

class SMSNotification implements Notification {
  @override
  void send() {
    print("SMS sent notification");
  }
}

class PushNotification implements Notification {
  @override
  void send() {
    print("Push sent notification");
  }
}

class NotificationFactory {
  static Notification createNotification(String type) {
    if (type == 'email') {
      return EmailNotification();
    } else if (type == 'sms') {
      return SMSNotification();
    } else if (type == 'push') {
      return PushNotification();
    } else {
      throw Exception("Notification type not found");
    }
  }
}