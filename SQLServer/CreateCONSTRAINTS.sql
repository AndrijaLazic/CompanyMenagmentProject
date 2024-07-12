USE [CompanyMenagmentProject]

ALTER TABLE [dbo].[WorkCalendar]  WITH CHECK ADD  CONSTRAINT [FK_WorkCalendar_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[WorkCalendar] CHECK CONSTRAINT [FK_WorkCalendar_Users]

ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_WorkerTypes] FOREIGN KEY([WorkerType])
REFERENCES [dbo].[WorkerTypes] ([Id])
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_WorkerTypes]