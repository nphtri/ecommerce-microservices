import './App.scss';

function App() {
  return (
    <div className="App">
      <div className="login">
        <div className="login-form">
          <form action="">
            <div className="form-field">
              <label htmlFor="username">Username</label>
              <input type="text" name="username" id="username" />
            </div>

            <div className="form-field">
              <label htmlFor="password">Password</label>
              <input type="password" name="password" id="password" />
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}

export default App;
