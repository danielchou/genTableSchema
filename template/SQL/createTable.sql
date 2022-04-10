CREATE TABLE [agdSet].[tb{tb}](
{colsStr}
 CONSTRAINT [PK_tb{tb}] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [agdSet].[tb{tb}] ADD  CONSTRAINT [DF_tb{tb}_Creator]  DEFAULT ((1)) FOR [Creator]
GO

{ColExtFormats}