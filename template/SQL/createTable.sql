CREATE TABLE [{pt_schema}].[tb{tb}](
{colsStr}
 CONSTRAINT [PK_tb{tb}] PRIMARY KEY CLUSTERED 
(
{tb_colsPK}
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

{pt_nonCluster}

ALTER TABLE [{pt_schema}].[tb{tb}] ADD  CONSTRAINT [DF_tb{tb}_Creator]  DEFAULT ((1)) FOR [Creator]
GO

{ColExtFormats}