import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, of, throwError } from "rxjs";
import { User } from "../models/user";
import { catchError, map } from "rxjs/operators";

const API_URL = "https://localhost:44337/api/user";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  private user = new BehaviorSubject<User>(null);
  private loggedIn = new BehaviorSubject<boolean>(false);

  get isLoggedIn() {
    return this.loggedIn.asObservable();
  }

  get loggedInUser() {
    return this.user.asObservable();
  }

  constructor(private http: HttpClient) {}

  // API: POST/login
  public login(user: User): Observable<User> {
    this.loggedIn.next(true);

    return this.http.post<User>(API_URL + "/login", user).pipe(
      map((result: User) => {
        this.user.next(result);
        return new User(result);
      }),
      catchError((err) => {
        this.loggedIn.next(false);
        this.user.next(null);
        return throwError(err);
      })
    );
  }

  // API: POST/login
  public register(user: User): Observable<User> {
    return this.http.post<User>(API_URL + "/register", user);
  }

  public logout() {
    this.loggedIn.next(false);
  }
}
