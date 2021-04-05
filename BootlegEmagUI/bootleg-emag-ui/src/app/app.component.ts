import { Component, OnChanges, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "./services/auth.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
})
export class AppComponent implements OnInit {
  title = "bootleg-emag";
  isLoggedIn$: boolean; 
  role = "";

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.role = this.authService.claims();
    this.authService.isLoggedIn.subscribe((value) => {
      this.isLoggedIn$ = value;
    });
  }

  logout() {
    this.authService.logout();
    this.role = "";
    this.router.navigateByUrl("/");
  }
}
