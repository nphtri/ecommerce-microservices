import { BrowserRouter as Router, Link, Route, Switch } from 'react-router-dom';

import './assets/css/App.scss';
import logo from './assets/images/Logo.svg';
import thearthouse from './assets/images/TheArtHouse.svg';
import searchIcon from './assets/images/Search.svg';
import cartIcon from './assets/images/Cart.svg';
import SearchBox from './features/searchbox/search-box';
import Home from './pages/Home';

function App() {
  return (
    <div className="App">
      <Router>
        <header>
          <div className="left">
            <div className="left-holder">
              <Link to="/">
                <img src={logo} alt="logo" height={16} className="unselectable" />
              </Link>
              <span>Limited Edition Art Prints, Luxury Lifestyle & Apparel</span>
            </div>
          </div>
          <div className="center">
            <Link to="/">
              <img src={thearthouse} alt="the art house" />
            </Link>
          </div>
          <div className="right">
            <div className="un-collapse">
              <a href="#" type="submit">
                <img src={searchIcon} alt="search-icon" height="20px" />
              </a>
              <SearchBox />
              <div style={{ height: '20px', borderLeft: 'solid 1px #000' }}></div>
              <span style={{ cursor: 'pointer' }}>Log in</span>
              <a href="#">
                <img src={cartIcon} alt="cart-icon" height="20px" />
              </a>
            </div>
            <div className="collapsed">
              <span></span>
            </div>
          </div>
        </header>
        <section id="header-nav-section">
          <nav className="header-nav">
            <ul>
              <li>
                <Link to="/arts">Arts</Link>
              </li>
              <li>
                <Link to="/artists">Artists</Link>
              </li>
              <li>
                <Link to="/collections">Collections</Link>
              </li>
              <li>
                <Link to="products">Products</Link>
              </li>
            </ul>
          </nav>
        </section>

        <Switch>
          <Route exact path="/" component={Home} />
        </Switch>

        <footer>
          <div id="footer-seperate"></div>
          <div id="footer-contain">
            <nav>
              <ul>
                <li>
                  <Link to="artist-application">Artist Applications</Link>
                </li>
                <li>
                  <Link to="about-us">About Us</Link>
                </li>
                <li>
                  <Link to="shipping">Shipping</Link>
                </li>
                <li>
                  <Link to="returns">Returns</Link>
                </li>
                <li>
                  <Link to="privacy-policy">Privacy Policy</Link>
                </li>
                <li>
                  <Link to="terms-n-conditions">Terms & Conditions</Link>
                </li>
                <li>
                  <Link to="trade-enqueries">Trade Enqueries</Link>
                </li>
                <li>
                  <Link to="contact-us">Contact Us</Link>
                </li>
              </ul>
            </nav>
            <div id="contact-span">
              <span>Call: +84 (0) 78 278 0684 Mon-Fri between 9am-5pm GMT</span>
            </div>
            <div id="footer-logo">
              <img src={logo} alt="footer-logo" height="50px" className="unselectable" />
            </div>
          </div>
        </footer>
      </Router>
    </div>
  );
}

export default App;
