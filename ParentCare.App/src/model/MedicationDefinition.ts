export class MedicationDefinition {
  Id: number;
  Title: string;
  Time: Date;
  Weekdays: number[];

  public constructor(init?:Partial<MedicationDefinition>) {
    Object.assign(this, init);
}
}

export class MedicationAlarm {
  Date: Date;
  Title: string;
  MedicationId: Number;
  Medication: MedicationDefinition;
  Taken: boolean;

  public constructor(init?:Partial<MedicationAlarm>) {
    Object.assign(this, init);
  }
}

