
# Scaffold-DbContext 生成实体和Context
## mysql
使用 VS 程序包管理器生成Context需先安装下边3个Nuget包
    MySql.EntityFrameworkCore
    Pomelo.EntityFrameworkCore.MySql     
    Microsoft.EntityFrameworkCore.Tools
执行下边命令(注意所在类库必须编译通过没有错误且设为启动项 否则执行失败)
    Scaffold-DbContext "Server=localhost;Database=dfconfig;Uid=root;Pwd=root" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -Force

## sqlserver


## oracle
