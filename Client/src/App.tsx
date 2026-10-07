import "./index.css";
import { Api, type BookDto } from "../Api.ts";
import { useEffect, useState } from "react";
import toast from "react-hot-toast";

const MyApi = new Api();

export function App() {
    const [books, setBooks] = useState<BookDto[]>([]);
    const [newBookTitle, setNewBookTitle] = useState("");

    useEffect(() => {
        MyApi.getBooks
            .libraryGetBooks({
                page: 1,
                resultsPerPage: 100
            })
            .then(r => {
                setBooks(r.data);
            })
            .catch(e => {
                toast.error(e.error?.title ?? "Could not load books");
            });
    }, []);

    function createBook() {
        if (!newBookTitle.trim()) {
            toast.error("Please enter a book title");
            return;
        }

        MyApi.createBook
            .libraryCreateBook({
                BookTitle: newBookTitle,
                AuthorId: "1",
                NumberOfPages: 100
            })
            .then(r => {
                setBooks([...books, r.data]);
                setNewBookTitle("");
                toast.success("Book created!");
            })
            .catch(e => {
                toast.error(e.error?.title ?? "Could not create book");
            });
    }

    return (
        <div>
            {books.map(b => (
                <div key={b.bookId}>
                    {b.bookTitle}
                </div>
            ))}

            <input
                value={newBookTitle}
                onChange={e => setNewBookTitle(e.target.value)}
            />

            <button onClick={createBook}>
                Create book
            </button>

            <div
                style={{
                    position: "absolute",
                    top: 35,
                    left: 35,
                }}
            >
                <img
                    src="https://1265745076.rsc.cdn77.org/1024/jpg/83356-6880e2d776240.jpg"
                    alt="White Satin"
                    width="120"
                    height="120"
                />
            </div>
        </div>
    );
}

export default App;
