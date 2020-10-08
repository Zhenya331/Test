import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";


@Injectable()
export class ImageService {

  private baseUrl = "/api/image";

  constructor(private http: HttpClient) { }

  findImage(formdata: FormData) {
    return this.http.post(this.baseUrl, formdata);
  }

  getImages(id: number) {
    return this.http.get(this.baseUrl + '/' + id);
  }

  deleteImage(id: number) {
    return this.http.delete(this.baseUrl + '/' + id);
  }

  addImage(UserId: number, formdata: FormData) {
    return this.http.post(this.baseUrl + '/' + UserId, formdata);
  }
}
