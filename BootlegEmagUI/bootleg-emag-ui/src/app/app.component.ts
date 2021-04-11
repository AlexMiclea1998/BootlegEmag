import { Component, OnChanges, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { User } from "./models/user";
import { AuthService } from "./services/auth.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
})
export class AppComponent implements OnInit {
  title = "bootleg-emag";
  isLoggedIn$: boolean; 
  user: User = null;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.authService.isLoggedIn.subscribe((value) => {
      this.isLoggedIn$ = value;
    });
    this.authService.loggedInUser.subscribe((value) => {
      this.user = value;
      console.log('user subscribe', this.user);
    });
  }

  logout() {
    this.authService.logout();
    this.user = null;
    this.router.navigateByUrl("/");
  }
}
