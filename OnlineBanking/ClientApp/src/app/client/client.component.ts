import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { parse } from 'url';
import { Router } from '@angular/router';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html'
})
export class ClientComponent implements OnInit {
  clientForm: FormGroup;
  http: HttpClient;
  baseUrl: any;
  headers: HttpHeaders;

  constructor(
    http: HttpClient,
    private _fb: FormBuilder,
    @Inject('BASE_URL') baseUrl: string,
    private router: Router
  ) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');
  }

  ngOnInit() {
    this.clientForm = this._fb.group({
      UserName: ['', Validators.required],
      FirstName: ['', Validators.required],
      LastName: ['', Validators.required],
      Email: [''],
      StreetAddress1: [''],
      StreetAddress2: [''],
      City: [''],
      Country: [''],
      PostCode: ['']
    });

  }

  createClient() {
    let param = {
      UserName: this.clientForm.get('UserName').value,
      FirstName: this.clientForm.get('FirstName').value,
      LastName: this.clientForm.get('LastName').value,
      Email: this.clientForm.get('Email').value,
      StreetAddress1: this.clientForm.get('StreetAddress1').value,
      StreetAddress2: this.clientForm.get('StreetAddress2').value,
      City: this.clientForm.get('City').value,
      Country: this.clientForm.get('Country').value,
      PostCode: this.clientForm.get('PostCode').value
    }
    this.http.post<any>(this.baseUrl + 'client/CreateClient', param, { headers: this.headers }).subscribe(result => {
      this.router.navigate(['/account-creation/' + result.clientId]);
    }, error => console.error(error));
  }
}
