import { BrowserRouter, Router } from 'react-router-dom';
import history from './Utils/history';
import Routes from './Routes/Routes';

function App() {
  return (
    <Router history={history}>
      <BrowserRouter>
        <Routes />
      </BrowserRouter>
    </Router>
  );
}

export default App;
