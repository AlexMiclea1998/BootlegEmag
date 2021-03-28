import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { User } from "../models/user";

const API_URL = "https://localhost:44337/api";

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    constructor(private http: HttpClient) {
    }

    // API: POST/login
    public login(user: User): Observable<User> {
        return this.http
            .post<User>(API_URL + '/login', user);
    }
}