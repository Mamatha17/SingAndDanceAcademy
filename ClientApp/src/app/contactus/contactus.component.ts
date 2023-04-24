import { Component, OnInit } from '@angular/core';
import { FormControl, FormBuilder, Validators } from '@angular/forms';


@Component({
  selector: 'app-contactus',
  templateUrl: './contactus.component.html',
  styleUrls: ['./contactus.component.css']
})
export class ContactusComponent implements OnInit {
  public showThanks = false;
  public showErrorMsg = false;


  constructor() { }

  ngOnInit(): void {
  }

  onSubmit() {
    let isNotValid = false;
    let inputCmps = document.getElementsByTagName('input');
    if (inputCmps) {
      for (var i = 0; i < inputCmps.length; i++) {
        console.log(inputCmps[i].name, inputCmps[i].value);
        if (!inputCmps[i].value) {
          isNotValid = true;
          break;
        }
      }
      if (!(<HTMLInputElement>document.getElementById('subject')).value) {
        isNotValid = true;
      }
    }

    if (!isNotValid) {
      this.showThanks = true;
      this.showErrorMsg = false;
    } else {
      this.showErrorMsg = true;
    }
  }
}
