import { BrowserRouter, Router } from 'react-router-dom';
import history from './Utils/history';
import Routes from './Routes/Routes';
import Navbar from './components/Navigation/Navbar';

function App() {
  return (
    <Router history={history}>
      <BrowserRouter>
        <Navbar />
        <Routes />
      </BrowserRouter>
    </Router>
  );
}

export default App;
