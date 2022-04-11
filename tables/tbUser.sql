CREATE TABLE [agdSet].[tbUser](
	[SeqNo] [int] IDENTITY(1,1)NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[UserName] [nvarchar](60) NOT NULL,
	[UserCode] [varchar](50) NOT NULL,
	[AgentLoginID] [varchar](20) NOT NULL,
	[AgentLoginCode] [varchar](20) NOT NULL,
	[EmployeeNo] [varchar](11) NOT NULL,
	[NickName] [nvarchar](50) NOT NULL,
	[EmpDept] [varchar](20) NOT NULL,
	[GroupID] [varchar](20) NULL,
	[OfficeEmail] [varchar](70) NOT NULL,
	[EmployedStatusCode] [varchar](1) NOT NULL,
	[IsSupervisor] [bit] NOT NULL,
	[B08Code1] [nvarchar](100) NULL,
	[B08Code2] [nvarchar](100) NULL,
	[B08Code3] [nvarchar](100) NULL,
	[B08Code4] [nvarchar](100) NULL,
	[B08Code5] [nvarchar](100) NULL,
	[IsEnable] [bit] NOT NULL,
	[CreateDT] [datetime2](7) NOT NULL,
	[Creator] [varchar](20) NOT NULL,
	[UpdateDT] [datetime2](7) NOT NULL,
	[Updator] [varchar](20) NOT NULL,
 CONSTRAINT [PK_tbUser] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [agdSet].[tbUser] ADD  CONSTRAINT [DF_tbUser_Creator]  DEFAULT ((1)) FOR [Creator]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水號' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'SeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用者帳號' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用者名稱 : 來源HR FullName' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用者代碼' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'UserCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CTI登入帳號' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'AgentLoginID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CTI登入代碼' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'AgentLoginCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'員工編號 : 預留, 來源HR' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'EmployeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用者暱稱' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'NickName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所屬單位 : 來源HR EmpDeptCode + EmpDeptName' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'EmpDept'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'群組代碼' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'GroupID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Email : 來源HR' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'OfficeEmail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'在職狀態代碼 : 來源HR, 1適用折薪, 2適用全薪, 3任用, 4留停, 5離職' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'EmployedStatusCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否為主管?' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'IsSupervisor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'B08_code1' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'B08Code1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'B08_code2' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'B08Code2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'B08_code3' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'B08Code3'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'B08_code4' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'B08Code4'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'B08_code5' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'B08Code5'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否啟用?' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'IsEnable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'UpdateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'Updator'
GO