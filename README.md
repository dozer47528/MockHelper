MockHelper
==========
helper you to mock sealed class and non-virtual method
帮助你Mock密封类和非虚函数

====how to use====
1. Build The MockHelper
2. Copy the MockHelper.exe, mock.txt and Mono.Cecil*.dll to your Test project
3. Modify these six file to 'always copy'
4. Edit the Post-build event command line of you Test project:  "$(TargetDir)MockHelper\MockHelper.exe"
5. Add the dll which your want to mock to mock.txt
The Test peoject in this solution is a runable demo.

====如何使用====
1、编译 MockHelper
2、复制 MockHelper.exe、mock.txt 和 Mono.Cecil*.dll 到你的测试项目中
3、修改这几个文件的属性，改成“总是复制”
4、编辑测试项目的后期生成事件命令行："$(TargetDir)MockHelper\MockHelper.exe"
5、把你需要 Mock 的 dll 增加到 mock.txt 中
本解决方案中的测试项目是一个可运行的 Demo