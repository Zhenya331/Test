import { Component } from '@angular/core';
import { ImageService } from '../Services/ImageService';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  providers: [ImageService]
})
export class HomeComponent {
  private url = "";
  private user: User = new User();
  private nullUser: boolean = false;
  private emptyErrorMessage: boolean;

  constructor(private imageService: ImageService) { }

  public uploadFile = (files) => {

    if (files.length === 0) { return; }

    let fileToUpload = <File>files[0];

    let reader = new FileReader();
    reader.readAsDataURL(fileToUpload);
    reader.onload = (event: any) => {
      this.url = event.target.result;
    }


    this.nullUser = false;
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.imageService.findImage(formData)
      .subscribe((result: any) => {
        this.user = result;
        if (this.user.errorMessage === "ok") { this.emptyErrorMessage = true; }
        else { this.emptyErrorMessage = false; }
      });

    this.nullUser = true;
  }
}

class User {
  public id: number;
  public lastName: string;
  public firstName: string;
  public fatherName: string;
  public errorMessage: string;
}
