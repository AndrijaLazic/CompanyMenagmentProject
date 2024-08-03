export class GlobalSettingsDTO {
  users: UserShort[] = [];
  shiftTypes: ShiftType[] = [];
  workerTypes: WorkerType[] = [];
}

export class WorkerType {
  id!: number;
  typeName!: string;
}

export class UserShort {
  id!: number;
  name!: string;
  lastname!: string;
  email!: string;
  phoneNumber!: string;
  workerType!: number;
}
export class ShiftType {
  shiftNumber!: number;
  startTime!: string;
  endTime!: string;
}
