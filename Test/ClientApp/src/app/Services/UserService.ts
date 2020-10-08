import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";

@Injectable()
export class UserService {

  private baseUrl = "/api/user";

  constructor(private http: HttpClient) { }

  findUsers(LastName: string, FirstName: string, FatherName: string) {
    let params = new HttpParams();
    if (LastName === "") { LastName = "_"; }
    if (FirstName === "") { FirstName = "_"; }
    if (FatherName === "") { FatherName = "_"; }
    params = params.append('lastname', LastName);
    params = params.append('firstname', FirstName);
    params = params.append('fathername', FatherName);
    return this.http.get(this.baseUrl, {params: params});
  }

  getUserProfile(id: number) {
    return this.http.get(this.baseUrl + '/' + id);
  }

  updateUser(id: number, LastName: string, FirstName: string, FatherName: string) {
    if (LastName === "") { LastName = "Nullable"; }
    if (FirstName === "") { FirstName = "Nullable"; }
    if (FatherName === "") { FatherName = "Nullable"; }

    return this.http.put(this.baseUrl + "/" + id + "/" + LastName + "/" + FirstName + "/" + FatherName, 0);
  }

  deleteUser(id: number) {
    return this.http.delete(this.baseUrl + '/' + id);
  }

  addUser(LastName: string, FirstName: string, FatherName: string) {
    if (LastName === "") { LastName = "Nullable"; }
    if (FirstName === "") { FirstName = "Nullable"; }
    if (FatherName === "") { FatherName = "Nullable"; }

    return this.http.post(this.baseUrl + "/" + LastName + "/" + FirstName + "/" + FatherName, 0);
  }
}
