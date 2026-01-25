export interface User {
    UserId:number,
    UserName: string;
    Token: string;
    Created: Date;
    paidTill: Date;
    password:string,
    Email:string,
    AllowedToSee:number[],
    gender:string,
    PhoneNumber:string
}