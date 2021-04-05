export enum Role {
    Admin = 'ADMIN',
    Shopper = 'SHOPPER',
    Seller = 'SELLER'
}

export class User {
    username: string;
    password: string;
    role: string;

    constructor(values: Object = {}) {
        Object.assign(this, values);
    }
}