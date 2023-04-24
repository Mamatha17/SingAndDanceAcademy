import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html',
  styleUrls: ['./album.component.css']
})

export class AlbumComponent {

  public albumsData: AlbumData[] = [];
  public updateStatus: String = '';

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.getAlbumsData();
  }

  /* Helper Methods */
  getAlbumsData() {
    this.http.get<AlbumData[]>(this.baseUrl + 'album').subscribe(result => {
      this.albumsData = result;
    });
  }

  onEdit(item: any) {
    item.action = 'edit';
  }

  onUpdate(item: any) {
    this.updateAlbumItem(item);
  }

  onCancel() {
    this.getAlbumsData();
  }

  updateAlbumItem(item: any) {
    const url = 'album';
    const body = item;
    const headers = { 'Content-Type': 'application/json' };
    this.http.patch<AlbumData[]>(this.baseUrl + url, item, { headers }).subscribe({
      next: result => {
        this.albumsData = result;
      },
      error: error => {
        alert('Updated Failed!');
      }
    })
  }

  onDelete(item: any) {
    const url = 'album/' + item.id;
    const headers = { 'Content-Type': 'application/json' };

    this.http.delete<AlbumData[]>(this.baseUrl + url, { headers }).subscribe({
      next: result => {
        this.getAlbumsData();
      },
      error: error => {
        alert('Delete Failed!');
      }
    })
  }

  onAdd(item: any) {
    if (item) {
      item.action = "";
      const url = 'album';
      const body = item;
      const headers = { 'Content-Type': 'application/json' };

      this.http.post<AlbumData[]>(this.baseUrl + url, item, { headers }).subscribe({
        next: result => {
          this.albumsData = result;
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
      title: "",
      year: "",
      genre: "",
      description: "",
      action: "add"
    }
    this.albumsData.unshift(emptyItem);
  }

}
interface AlbumData {
  id: String;
  title: String;
  year: String;
  genre: String;
  description: String;
  action: string;
}
