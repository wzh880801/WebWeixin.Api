# .NET SDK For Web Weixin

       +--------------+     +---------------+   +---------------+
       |              |     |               |   |               |
       |   Get UUID   |     |  Get Contact  |   | Status Notify |
       |              |     |               |   |               |
       +-------+------+     +-------^-------+   +-------^-------+
               |                    |                   |
               |                    +-------+  +--------+
               |                            |  |
       +-------v------+               +-----+--+------+      +--------------+
       |              |               |               |      |              |
       |  Get QRCode  |               |  Weixin Init  +------>  Sync Check  <----+
       |              |               |               |      |              |    |
       +-------+------+               +-------^-------+      +-------+------+    |
               |                              |                      |           |
               |                              |                      +-----------+
               |                              |                      |
       +-------v------+               +-------+--------+     +-------v-------+
       |              | Confirm Login |                |     |               |
+------>    Login     +---------------> New Login Page |     |  Weixin Sync  |
|      |              |               |                |     |               |
|      +------+-------+               +----------------+     +---------------+
|             |
|QRCode Scaned|
+-------------+

实现的过程中参考的一些资料：

1. https://segmentfault.com/a/1190000004471678
1. http://blog.csdn.net/wonxxx/article/details/51787041

Demo中演示了如何加载登录二维码，如何检测是否扫码，如何登录，如何打开微信状态通知，如何加载联系人，如何接受、发送消息（文本、图片），如何上传文件。

SDK支持wx2.qq.com和wx.qq.com 2个域名，这2个域名分别对应IOS和Android微信APP扫码后的跳转地址，SDK中会自动处理域名的转换。

#Usage:

    IWeixinClient client = new DefaultWeixinClient();
    var request = new Entity.***Request();
    var response = await client.ExecuteAsync(request);