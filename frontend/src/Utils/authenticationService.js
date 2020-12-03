
import { api } from '../axiosInstance';

export async function login(username, password) {
  const data = { username, password };
  const user = await api.post('/User/Login', data);
  localStorage.setItem('currentUser', JSON.stringify(user));
}
export function currentUserValue() {
    let user = null;
    if(localStorage.getItem('currentUser') !== "undefined"){
      user = JSON.parse(localStorage.getItem('currentUser'));
    }
    return user;
}
export function logout() {
  // remove user from local storage to log user out
  localStorage.removeItem('currentUser');
}
