export class Caretaker {
  Id: Number;
  Name: string;
  Phone: string;
  Email: string;

  public constructor(init?:Partial<Caretaker>) {
    Object.assign(this, init);
}
}
