import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { User } from "../models/user";
import { AuthService } from "../services/auth.service";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  user: User;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ["", Validators.required],
      password: ["", Validators.required],
    });
  }

  login() {
    debugger;
    this.user = new User({
      username: this.loginForm.value.username,
      password: this.loginForm.value.password,
    });
    if (this.user) {
      this.authService.login(this.user).subscribe(() => {
        console.log("User is logged in");
        // this.router.navigateByUrl('/');
        // TODO: implement service and set user claims instead of reloading the page
        window.location.reload();
      });
    }
  }
}
