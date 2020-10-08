import { Component } from "@angular/core";
import { UserService } from "../Services/UserService";


@Component({
  selector: 'app-find-users',
  templateUrl: './find-users.component.html',
  providers: [UserService]
})
export class FindUsersComponent {

  private lastName: string = "";
  private firstName: string = "";
  private fatherName: string = "";

  private users: User[];

  private checkNotNull: boolean = false;
  private getResult: boolean = false;

  constructor(private userService: UserService) { }

  findUsers() {
    this.users = []
    this.userService.findUsers(this.lastName, this.firstName, this.fatherName)
      .subscribe((result: any) => {
        for (var res of result) {
          this.users.push(new User(res.id, res.lastName, res.firstName, res.fatherName));
        }
        this.checkNotNull = this.users.length > 0;
        this.getResult = true;
      });
  }
}

class User {
  constructor(public id: number, public lastName: string, public firstName: string, public fatherName: string) { }
}
