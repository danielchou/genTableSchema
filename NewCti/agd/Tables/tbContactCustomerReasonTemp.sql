CREATE TABLE [agd].[tbContactCustomerReasonTemp](
	[ContactID] [int](50)  NOT NULL,	/*服務紀錄ID*/
	[ContactSeq] [tinyint]  NOT NULL,	/*序號*/
	[Memo] [nvarchar](200)  NULL,	/*備註*/
	[ReasonID1] [varchar](20)  NOT NULL,	/*聯繫原因大類ID*/
	[ReasonName1] [nvarchar](50)  NOT NULL,	/*聯繫原因大類名稱*/
	[ReasonID2] [varchar](20)  NOT NULL,	/*聯繫原因中類ID*/
	[ReasonName2] [nvarchar](50)  NOT NULL,	/*聯繫原因中類名稱*/
	[ReasonID3] [varchar](20)  NOT NULL,	/*聯繫原因小類ID*/
	[ReasonName3] [nvarchar](50)  NOT NULL,	/*聯繫原因小類名稱*/
	[IsPrimary] [bit]  NOT NULL,	/*是否為主要?*/
	[ReviewType] [varchar](3)  NULL,	/*日終覆核類別代碼*/
	[CreateDT] [datetime2](7)  NOT NULL,	/*建立時間*/
	[Creator] [varchar](11)  NOT NULL,	/*建立者*/
	[CreatorName] [nvarchar](60)  NOT NULL,	/*建立人員*/
 CONSTRAINT [PK_tbContactCustomerReasonTemp] PRIMARY KEY CLUSTERED 
(
	[ContactID] ASC,
	[ContactSeq] ASC,
	[ReasonID3] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服務紀錄ID' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbContactCustomerReasonTemp', @level2type=N'COLUMN',@level2name=N'ContactID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序號' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbContactCustomerReasonTemp', @level2type=N'COLUMN',@level2name=N'ContactSeq'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'備註' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbContactCustomerReasonTemp', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'聯繫原因大類ID' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbContactCustomerReasonTemp', @level2type=N'COLUMN',@level2name=N'ReasonID1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'聯繫原因大類名稱' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbContactCustomerReasonTemp', @level2type=N'COLUMN',@level2name=N'ReasonName1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'聯繫原因中類ID' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbContactCustomerReasonTemp', @level2type=N'COLUMN',@level2name=N'ReasonID2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'聯繫原因中類名稱' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbContactCustomerReasonTemp', @level2type=N'COLUMN',@level2name=N'ReasonName2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'聯繫原因小類ID' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbContactCustomerReasonTemp', @level2type=N'COLUMN',@level2name=N'ReasonID3'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'聯繫原因小類名稱' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbContactCustomerReasonTemp', @level2type=N'COLUMN',@level2name=N'ReasonName3'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否為主要?' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbContactCustomerReasonTemp', @level2type=N'COLUMN',@level2name=N'IsPrimary'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日終覆核類別代碼' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbContactCustomerReasonTemp', @level2type=N'COLUMN',@level2name=N'ReviewType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbContactCustomerReasonTemp', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbContactCustomerReasonTemp', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立人員' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbContactCustomerReasonTemp', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO