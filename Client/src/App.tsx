import { APITester } from "./APITester";
import "./index.css";

import logo from "./logo.svg";
import reactLogo from "./react.svg";
import {Api, type BookDto} from "../Api.ts";
import {useEffect, useState} from "react";
import toast from "react-hot-toast";

const MyApi = new Api();

export function App() {

    const [books, setBooks] = useState<BookDto[]>([])
    const [newBookTitle, setNewBookTitle] = useState("")

    useEffect(() => {
        MyApi.getBooks.libraryGetBooks({page: 1,
            resultsPerPage: 1}).then(r => {
            const data = r.data;
            setBooks(data);
        })
    }, []);

    function createBook() {
        MyApi.createBook.libraryCreateBook({
            BookTitle: newBookTitle,
            AuthorId: "1",
            NumberOfPages: 100
        }).then(r => {
            const duplicate = [...books, r.data];
            setBooks(duplicate);
        }).catch(e => {
            toast(e.error.title)
        })
    }

        return <div>

            {books.map(b => {return <div key={b.bookId}>{b.bookTitle} </div>})}
            <input value={newBookTitle} onChange={e => setNewBookTitle(e.target.value)}  />
            <button onClick={createBook}>Create book</button>

        <div style={{position: "absolute", top:35, left:35,}}>
            <img src={"https://1265745076.rsc.cdn77.org/1024/jpg/83356-6880e2d776240.jpg"} alt={"White Satin"} width={"120"} height={"120"}/>
        </div>



    </div>

}

export default App;
