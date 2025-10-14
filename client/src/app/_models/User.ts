export interface User {
    UserId:number,
    UserName: string;
    Token: string;
    Created: Date;
    paidTill: Date;
    password:string,
    Email:string,
    AllowedToSee:string,
    gender:string,
    PhoneNumber:string
}