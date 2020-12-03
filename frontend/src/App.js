import { BrowserRouter, Router } from 'react-router-dom';
import history from './Utils/history';
import Routes from './Routes/Routes';
import Navbar from './components/Navigation/Navbar';
import { SnackbarProvider } from 'notistack';

function App() {
  return (
    <SnackbarProvider maxSnack={3}>
      <Router history={history}>
        <BrowserRouter>
          <Navbar />
          <Routes />
        </BrowserRouter>
      </Router>
    </SnackbarProvider>
  );
}

export default App;
