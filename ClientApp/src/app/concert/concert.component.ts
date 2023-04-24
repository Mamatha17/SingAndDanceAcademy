import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-concert',
  templateUrl: './concert.component.html',
  styleUrls: ['./concert.component.css']
})
export class ConcertComponent {

  public concertData: ConcertData[] = [];
  public updateStatus: String = '';


  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.getconcertData();
  }

  /* Helper Methods */
  getconcertData() {
    this.http.get<ConcertData[]>(this.baseUrl + 'concert').subscribe(result => {
      this.concertData = result;
    });
  }

  onEdit(item: any) {
    item.action = 'edit';
  }

  onUpdate(item: any) {
    this.updateConcertItem(item);
  }

  onCancel() {
    this.getconcertData();
  }

  updateConcertItem(item: any) {
    const url = 'concert';
    const body = item;
    const headers = { 'Content-Type': 'application/json' };
    this.http.patch<ConcertData[]>(this.baseUrl + url, item, { headers }).subscribe({
      next: result => {
        this.concertData = result;
      },
      error: error => {
        alert('Updated Failed!');
      }
    })
  }

  onDelete(item: any) {
    const url = 'concert/' + item.id;
    const headers = { 'Content-Type': 'application/json' };

    this.http.delete<ConcertData[]>(this.baseUrl + url, { headers }).subscribe({
      next: result => {
        this.getconcertData();
      },
      error: error => {
        alert('Delete Failed!');
      }
    })
  }

  onAdd(item: any) {
    if (item) {
      item.action = "";
      const url = 'concert';
      const body = item;
      const headers = { 'Content-Type': 'application/json' };

      this.http.post<ConcertData[]>(this.baseUrl + url, item, { headers }).subscribe({
        next: result => {
          this.concertData = result;
        },
        error: error => {
          alert('Add Failed!');
        }
      })
    }
  }

  onAddEmptyRow() {
    const emptyItem = {
      id: "",
      name: "",
      composer: "",
      date: "",
      duration: "",
      location: "",
      action: "add"
    }
    this.concertData.unshift(emptyItem);
  }
}

interface ConcertData {
  id: String;
  name: String;
  composer: String;
  date: String;
  duration: String;
  location: String;
  action: string;

}
