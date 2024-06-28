USE [CompanyMenagmentProject]

ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_WorkerTypes] FOREIGN KEY([WorkerType])
REFERENCES [dbo].[WorkerTypes] ([Id])
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_WorkerTypes]

ALTER TABLE [dbo].[WorkCalendar]  WITH CHECK ADD  CONSTRAINT [FK_WorkCalendar_ShiftTypes] FOREIGN KEY([Shift])
REFERENCES [dbo].[ShiftTypes] ([ShiftNumber])
ALTER TABLE [dbo].[WorkCalendar] CHECK CONSTRAINT [FK_WorkCalendar_ShiftTypes]

ALTER TABLE [dbo].[WorkCalendar]  WITH CHECK ADD  CONSTRAINT [FK_WorkCalendar_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[WorkCalendar] CHECK CONSTRAINT [FK_WorkCalendar_Users]

