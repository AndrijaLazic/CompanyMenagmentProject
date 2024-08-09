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


--UserCommunication
ALTER TABLE [dbo].[UserCommunication] ADD  CONSTRAINT [DF_UserCommunication_User1Unread]  DEFAULT ((0)) FOR [User1Unread]
GO
ALTER TABLE [dbo].[UserCommunication] ADD  CONSTRAINT [DF_UserCommunication_User2Unread]  DEFAULT ((0)) FOR [User2Unread]
GO
ALTER TABLE [dbo].[UserCommunication]  WITH CHECK ADD  CONSTRAINT [FK_UserCommunication_Users1] FOREIGN KEY([User1])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserCommunication]  WITH CHECK ADD  CONSTRAINT [FK_UserCommunication_Users2] FOREIGN KEY([User2])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserCommunication]  WITH CHECK ADD  CONSTRAINT [CHK_User1NotEqualToUser2] CHECK  (([User1]<>[User2]))
GO


--User messages
ALTER TABLE [dbo].[CommunicationMessages]  WITH CHECK ADD  CONSTRAINT [FK_CommunicationMessages_UserCommunication] FOREIGN KEY([CommunicationId])
REFERENCES [dbo].[UserCommunication] ([Id])
GO
ALTER TABLE [dbo].[CommunicationMessages]  WITH CHECK ADD  CONSTRAINT [FK_CommunicationMessages_Users] FOREIGN KEY([SenderId])
REFERENCES [dbo].[Users] ([Id])
GO