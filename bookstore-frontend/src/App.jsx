import { useEffect, useState } from "react";
import "./App.css";

const API_URL = "http://localhost:5295/api";

function App() {
  const [books, setBooks] = useState([]);
  const [authors, setAuthors] = useState([]);
  const [error, setError] = useState("");
  const [editingId, setEditingId] = useState(null);

  const [form, setForm] = useState({
    title: "",
    price: "",
    authorId: "",
  });

  async function fetchData() {
    try {
      setError("");

      const booksResponse = await fetch(`${API_URL}/Books`);
      const authorsResponse = await fetch(`${API_URL}/Authors`);

      if (!booksResponse.ok || !authorsResponse.ok) {
        throw new Error("API svarar inte just nu.");
      }

      const booksData = await booksResponse.json();
      const authorsData = await authorsResponse.json();

      setBooks(booksData);
      setAuthors(authorsData);
    } catch {
      setError("Kunde inte hämta data från API:t. Kontrollera att backend körs.");
    }
  }

  useEffect(() => {
    fetchData();
  }, []);

  function handleChange(e) {
    setForm({
      ...form,
      [e.target.name]: e.target.value,
    });
  }

  async function handleSubmit(e) {
    e.preventDefault();

    if (!form.title || !form.price || !form.authorId) {
      setError("Fyll i titel, pris och författare.");
      return;
    }

    const bookData = {
      title: form.title,
      price: Number(form.price),
      authorId: Number(form.authorId),
    };

    try {
      setError("");

      const url = editingId
        ? `${API_URL}/Books/${editingId}`
        : `${API_URL}/Books`;

      const method = editingId ? "PUT" : "POST";

      const response = await fetch(url, {
        method,
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(bookData),
      });

      if (!response.ok) {
        throw new Error("Något gick fel.");
      }

      setForm({
        title: "",
        price: "",
        authorId: "",
      });

      setEditingId(null);
      fetchData();
    } catch {
      setError("Kunde inte spara boken.");
    }
  }

  function startEdit(book) {
    const author = authors.find((a) => a.name === book.authorName);

    setEditingId(book.id);

    setForm({
      title: book.title,
      price: book.price,
      authorId: author ? author.id : "",
    });
  }

  async function deleteBook(id) {
    try {
      setError("");

      const response = await fetch(`${API_URL}/Books/${id}`, {
        method: "DELETE",
      });

      if (!response.ok) {
        throw new Error("Kunde inte ta bort.");
      }

      fetchData();
    } catch {
      setError("Kunde inte ta bort boken.");
    }
  }

  return (
    <main className="app">
      <nav className="navbar">
        <div>
          <p className="eyebrow">Fullstack C# II</p>
          <h1>BookStore Admin</h1>
        </div>

        <div className="nav-links">
          <a href="#books">Böcker</a>
          <a href="#create">Skapa</a>
          <a href="#tech">Tech</a>
        </div>
      </nav>

      <section className="hero">
        <div>
          <p className="badge">React + .NET Web API + SQL Server</p>
          <h2>Hantera böcker och författare i ett modernt adminsystem.</h2>
          <p className="hero-text">
            En fullstack-applikation byggd med React frontend, ASP.NET Core Web API,
            Entity Framework Core, Repository Pattern och xUnit-tester.
          </p>
        </div>

        <div className="stats-card">
          <span>Backend status</span>
          <strong>CRUD Ready</strong>
          <p>Books + Authors med 1-många relation</p>
        </div>
      </section>

      {error && <div className="error">{error}</div>}

      <section className="grid">
        <div className="panel" id="create">
          <h3>{editingId ? "Uppdatera bok" : "Skapa ny bok"}</h3>

          <form onSubmit={handleSubmit}>
            <label>Titel</label>
            <input
              name="title"
              value={form.title}
              onChange={handleChange}
              placeholder="Ex. Clean Code"
            />

            <label>Pris</label>
            <input
              name="price"
              type="number"
              value={form.price}
              onChange={handleChange}
              placeholder="Ex. 299"
            />

            <label>Författare</label>
            <select
              name="authorId"
              value={form.authorId}
              onChange={handleChange}
            >
              <option value="">Välj författare</option>
              {authors.map((author) => (
                <option key={author.id} value={author.id}>
                  {author.name}
                </option>
              ))}
            </select>

            <button type="submit">
              {editingId ? "Spara ändringar" : "Skapa bok"}
            </button>

            {editingId && (
              <button
                type="button"
                className="secondary"
                onClick={() => {
                  setEditingId(null);
                  setForm({ title: "", price: "", authorId: "" });
                }}
              >
                Avbryt
              </button>
            )}
          </form>
        </div>

        <div className="panel" id="books">
          <div className="panel-header">
            <h3>Böcker</h3>
            <span>{books.length} st</span>
          </div>

          <div className="book-list">
            {books.length === 0 ? (
              <p className="empty">Inga böcker finns ännu.</p>
            ) : (
              books.map((book) => (
                <article className="book-card" key={book.id}>
                  <div>
                    <h4>{book.title}</h4>
                    <p>{book.authorName}</p>
                  </div>

                  <strong>{book.price} kr</strong>

                  <div className="actions">
                    <button onClick={() => startEdit(book)}>Ändra</button>
                    <button className="danger" onClick={() => deleteBook(book.id)}>
                      Ta bort
                    </button>
                  </div>
                </article>
              ))
            )}
          </div>
        </div>
      </section>

      <section className="tech" id="tech">
        <h3>Projektkrav uppfyllda</h3>
        <div className="tech-grid">
          <span>React frontend</span>
          <span>.NET Web API</span>
          <span>SQL Server</span>
          <span>EF Core</span>
          <span>Repository Pattern</span>
          <span>xUnit + NSubstitute</span>
        </div>
      </section>
    </main>
  );
}

export default App;