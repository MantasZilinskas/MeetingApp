import {
  Avatar,
  Box,
  Divider,
  IconButton,
  List,
  ListItem,
  ListItemAvatar,
  ListItemText,
  makeStyles,
  TextField,
} from '@material-ui/core';
import CloseIcon from '@material-ui/icons/Close';
import Autocomplete from '@material-ui/lab/Autocomplete';
import Skeleton from '@material-ui/lab/Skeleton';
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { api } from '../../../axiosInstance';

const useStyles = makeStyles((theme) => ({
  root: {
    width: '100%',
    maxWidth: '36ch',
    backgroundColor: theme.palette.background.paper,
    maxHeight: '450px',
    minWidth: '250px',
    overflow: 'auto',
  },
  inline: {
    display: 'inline',
  },
  iconButton: {
    marginRight: theme.spacing(1),
  },
}));

export default function UserSelect() {
  const [meetingUsers, setMeetingUsers] = useState([]);
  const [users, setUsers] = useState([]);
  const [isLoading, setLoading] = useState(false);
  const [isUsersLoading, setUsersLoading] = useState(false);
  const { meetingId } = useParams();
  const classes = useStyles();

  const deleteListItem = async (user) => {
    setLoading(true);
    const newList = meetingUsers.filter((element) => element.id !== user.id);
    setMeetingUsers(newList);
    await api.delete(`meeting/${meetingId}/users/${user.id}`);
    setLoading(false);
  };
  const selectUser = async (event, user) => {
    if (user && !meetingUsers.some((element) => element.id === user.id)) {
      setLoading(true);
      const currentState = meetingUsers.slice();
      currentState.push(user);
      setMeetingUsers(currentState);
      await api.post(`meeting/${meetingId}/users/${user.id}`);
      setLoading(false);
    }
  };

  const fetchData = async () => {
    setUsersLoading(true);
    const result = await api.get('user');
    const meetingUsersResponse = await api.get(`meeting/${meetingId}/users`)
    setUsers(result);
    setMeetingUsers(meetingUsersResponse);
    setUsersLoading(false);
  };
  useEffect(() => {
    fetchData();
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [meetingId]);
  return (
    <>
      {isUsersLoading ? (
        <Skeleton animation="wave" variant="rect" height={60} />
      ) : (
        <Autocomplete
          clearOnEscape
          options={users}
          onChange={selectUser}
          getOptionLabel={(option) => option.userName + ' - ' + option.fullName}
          getOptionSelected={(option, value) => option.id === value.id}
          renderInput={(params) => (
            <TextField {...params} label={'Select User'} />
          )}
        />
      )}

      <List className={classes.root}>
        {meetingUsers.map((user) =>
          isLoading ? (
            <Skeleton animation="wave" variant="rect" height={60} />
          ) : (
            <Box key={user.id}>
              <ListItem alignItems="center" key={user.id} >
                <ListItemAvatar>
                  <Avatar alt="Remy Sharp" src="./PlaceholderProfile" />
                </ListItemAvatar>
                <ListItemText primary={user.fullName} />
                <IconButton
                  onClick={() => deleteListItem(user)}
                  className={classes.iconButton}
                >
                  <CloseIcon />
                </IconButton>
              </ListItem>
              <Divider variant="inset" component="li" />
            </Box>
          )
        )}
      </List>
    </>
  );
}
