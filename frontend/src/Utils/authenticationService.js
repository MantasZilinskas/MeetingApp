import { BehaviorSubject } from 'rxjs';
import { api } from '../axiosInstance';

const currentUserSubject = new BehaviorSubject(
  localStorage.getItem('currentUser')
);

export const auth = {
  login,
  logout,
  currentUser: currentUserSubject.asObservable(),
  get currentUserValue() {
    return currentUserSubject.value;
  },
};

async function login(username, password) {
  const data = { username, password };
  const user = await api.post('/User/Login', data);
  localStorage.setItem('currentUser', JSON.stringify(user));
  currentUserSubject.next(user);
}

function logout() {
  // remove user from local storage to log user out
  localStorage.removeItem('currentUser');
  currentUserSubject.next(null);
}
