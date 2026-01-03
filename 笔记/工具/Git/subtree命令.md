# subtree命令

需求：仓库A需要使用仓库B中的某些文件。

解决方案：将仓库B中的目标文件分离成独立分支，在仓库A中引用该分支。此后，只需要维护仓库B的master分支，然后将变化提交到独立分支即可。

首先，将仓库B中的目标文件分离成独立分支。

```bash
# 在仓库B根目录下执行拆分命令，将目标文件夹拆分到独立分支
git subtree split --prefix=目标文件夹 -b 独立分支
# 将拆分后的分支推送到远程仓库
git push origin 独立分支
```

其次，仓库A关联仓库A的独立分支。

```bash
# 在仓库A根目录下执行添加命令，将仓库B的独立分支添加到仓库A的指定路径
git subtree add --prefix=指定路径 仓库B的git路径 独立分支 --squash
```

后续仓库B的修改和同步。

```bash
# 在仓库B中的master分支上修改了相应文件后需要先提交修改
git add 目标文件夹
git commit -m "提交日志"
# 同步到独立分支
git subtree split --prefix=目标文件夹 -b 独立分支 --rejoin
# 推送到远程仓库
git push origin 独立分支
```

最后，仓库A同步仓库B中的修改。

```bash
# 在仓库A根目录下执行拉取命令，将仓库B的独立分支同步到仓库A的指定路径
git subtree pull --prefix=指定路径 仓库B的git路径 独立分支 --squash
# 推送到远程仓库
git push origin master
```

## 报错

在上述操作中，可能会遇到`fatal: assertion failed: test "347254224350256260" = 笔记`报错，主要原因是路径/文件名中保护中文导致编码或解析冲突，需要配置Git支持中文编码。

```bash
# 配置Git显示中文文件名（避免乱码，同时让Git能正确解析中文路径）
git config --global core.quotepath false
# 配置Git的字符编码为UTF-8（兼容中文）
git config --global i18n.commit.encoding utf-8
git config --global i18n.logoutputencoding utf-8
# （Windows系统额外配置）设置环境变量支持UTF-8编码
export LC_ALL=en_US.UTF-8
export LANG=en_US.UTF-8
```

如果配置Git支持中文编码仍然报错，则只能修改路径和文件名，规避中文。
