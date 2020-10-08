import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { switchMap } from 'rxjs/operators';
import { UserService } from "../Services/UserService";
import { ImageService } from "../Services/ImageService";


@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css'],
  providers: [UserService, ImageService]
})
export class UserProfileComponent implements OnInit {

  private id: number;
  private lastName: string;
  private firstName: string;
  private fatherName: string;

  private forUpdateUser: boolean = true;
  private showimages: boolean = false;

  private images: Image[] = [];
  private url = "";
  private fileToUpload: File;

  constructor(private navigation: Router, private route: ActivatedRoute, private imageService: ImageService, private userService: UserService) { }

  ngOnInit() {
    document.getElementById("saveChanges").style.visibility = "hidden";

    this.route.paramMap.pipe(switchMap(params => params.getAll('id')))
      .subscribe(data => this.id = +data);

    this.userService.getUserProfile(this.id).subscribe((result: any) => {
      this.lastName = result.lastName;
      this.firstName = result.firstName;
      this.fatherName = result.fatherName;
    });
  }

  ChangeName() {
    document.getElementById("changeName").style.visibility = "hidden";
    document.getElementById("saveChanges").style.visibility = "visible";
    this.forUpdateUser = false;
  }
  SaveChanges() {
    this.userService.updateUser(this.id, this.lastName, this.firstName, this.fatherName)
      .subscribe((result: string) => {
        console.log(result);
      });

    this.forUpdateUser = true;
    document.getElementById("saveChanges").style.visibility = "hidden";
    document.getElementById("changeName").style.visibility = "visible";
    this.navigation.navigate(['/user-profile', this.id]);
  }

  DeleteUser() {
    this.userService.deleteUser(this.id).subscribe((result: string) => {
      console.log(result);
    });
    this.navigation.navigate(['']);
  }

  ShowPhoto() {
    this.images = [];
    this.showimages = true;

    this.imageService.getImages(this.id).subscribe((result: any) => {
      for (var res of result) {
        this.images.push(new Image(res.id, res.userId, "data:image/jpeg;base64," + res.imageData));
      }
    });
  }

  public uploadFile = (files) => {
    if (files.length === 0) { return; }
    this.fileToUpload = <File>files[0];

    let reader = new FileReader();
    reader.readAsDataURL(this.fileToUpload);
    reader.onload = (event: any) => {
      this.url = event.target.result;
    }
  }

  AddImage() {
    const formData = new FormData();
    formData.append('file', this.fileToUpload, this.fileToUpload.name);

    this.imageService.addImage(this.id, formData).subscribe((result: string) => {
      console.log(result);
    });
    this.navigation.navigate(['/user-profile', this.id]);
  }

  DeleteImage(ImageId: number) {
    this.imageService.deleteImage(ImageId).subscribe((result: string) => {
      console.log(result);
    });
    this.navigation.navigate(['/user-profile', this.id]);
  }
}

class Image {
  constructor(public id: number, public userId: number, public url: string) {}
}
