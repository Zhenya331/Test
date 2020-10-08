import { Component } from "@angular/core";
import { UserService } from "../Services/UserService";
import { Router } from "@angular/router";

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  providers: [UserService]
})
export class AddUserComponent {

  private lastName: string = "";
  private firstName: string = "";
  private fatherName: string = "";

  constructor(private navigation: Router, private userService: UserService) { }

  CreateUser() {
    this.userService.addUser(this.lastName, this.firstName, this.fatherName)
      .subscribe((result: string) => {
        console.log(result);
      });
    this.navigation.navigate(['']);
  }
}
