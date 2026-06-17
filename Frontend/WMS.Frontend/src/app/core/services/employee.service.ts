import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { environment } from '../../../environments/environment';



@Injectable({

  providedIn: 'root'

})

export class EmployeeService {



  constructor(

    private http: HttpClient

  ) {}



  getAll() {

    return this.http.get(

      `${environment.apiUrl}/Employees`

    );

  }



  getById(id:number) {

    return this.http.get(

      `${environment.apiUrl}/Employees/${id}`

    );

  }



  create(data:any) {

    return this.http.post(

      `${environment.apiUrl}/Employees`,

      data

    );

  }

  searchByName(name:string){
  return this.http.get(
    `${environment.apiUrl}/Employees/search/name/${name}`
  );
}

searchByDepartment(id:number){
  return this.http.get(
    `${environment.apiUrl}/Employees/search/department/${id}`
  );
}

searchByRole(id:number){
  return this.http.get(
    `${environment.apiUrl}/Employees/search/role/${id}`
  );
}

  update(id:number,data:any) {

    return this.http.put(

      `${environment.apiUrl}/Employees/${id}`,

      data

    );

  }



  delete(id:number) {

    return this.http.delete(

      `${environment.apiUrl}/Employees/${id}`

    );

  }

}