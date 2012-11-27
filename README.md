MockHelper
==========


帮助你Mock密封类和非虚函数


====如何使用====

1. 编译 MockHelper

2. 复制 MockHelper.exe、mock.txt 和 Mono.Cecil*.dll 到你的测试项目中

3. 把你需要 Mock 的 dll 增加到 mock.txt 中

4. 编辑测试项目的后期生成事件命令行："$(ProjectDir)MockHelper\MockHelper.exe" [包括双引号！]

5. 测试—编辑测试设置—（两类设置都要设置一下）—部署—取消启用部署 [如果不是 MSTest 可以不设置]

本解决方案中的测试项目是一个可运行的 Demo