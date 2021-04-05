import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, of } from "rxjs";
import { User } from "../models/user";
import { catchError, map } from "rxjs/operators";

const API_URL = "https://localhost:44337/api/user";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  userRole = "";
  private loggedIn = new BehaviorSubject<boolean>(false); // {1}

  get isLoggedIn() {
    return this.loggedIn.asObservable(); // {2}
  }

  constructor(private http: HttpClient) {}

  // API: POST/login
  public login(user: User): Observable<User> {
    this.userRole = user.role;
    this.loggedIn.next(true);
    return this.http
      .post<User>(API_URL + "/login", user)
      .pipe(catchError(this.handleError<User>(`login`)));
  }

  // API: POST/login
  public register(user: User): Observable<User> {
    return this.http.post<User>(API_URL + "/register", user);
  }

  public logout() {
    this.userRole = "";
    this.loggedIn.next(false);
  }

  public claims(): string {
    return this.userRole;
  }

  private handleError<T>(operation = "operation", result?: T) {
    if (operation === "login") {
      this.userRole = "";
    //   this.loggedIn.next(false);
    }
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
