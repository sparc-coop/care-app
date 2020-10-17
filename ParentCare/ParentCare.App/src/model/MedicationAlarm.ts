export class MedicationAlarm {
  Title: string;
  Time: Date;
  Weekdays: number[];

  public constructor(init?:Partial<MedicationAlarm>) {
    Object.assign(this, init);
}
}
