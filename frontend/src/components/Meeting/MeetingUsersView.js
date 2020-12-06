import {
  Avatar,
  Box,
  CircularProgress,
  Divider,
  List,
  ListItem,
  ListItemAvatar,
  ListItemText,
  makeStyles,
} from '@material-ui/core';
import Skeleton from '@material-ui/lab/Skeleton';
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { api } from '../../axiosInstance';

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
  center: {
    position: 'fixed',
    top: '50%',
    left: '50%',
  },
}));

export default function MeetingUsersView() {
  const [meetingUsers, setMeetingUsers] = useState([]);
  const [isLoading, setLoading] = useState(false);
  const { meetingId } = useParams();
  const classes = useStyles();

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      const meetingUsersResponse = await api.get(`meeting/${meetingId}/users`);
      setMeetingUsers(meetingUsersResponse);
      setLoading(false);
    };
    fetchData();
  }, [meetingId]);

  if (isLoading) {
    return <CircularProgress className={classes.center} />;
  }
  return (
    <>
      <List className={classes.root}>
        {meetingUsers.map((user) =>
          isLoading ? (
            <Skeleton animation="wave" variant="rect" height={60} />
          ) : (
            <Box key={user.id}>
              <ListItem alignItems="center" key={user.id}>
                <ListItemAvatar>
                  <Avatar alt="Remy Sharp" src="./PlaceholderProfile" />
                </ListItemAvatar>
                <ListItemText primary={user.fullName} />
              </ListItem>
              <Divider variant="inset" component="li" />
            </Box>
          )
        )}
      </List>
    </>
  );
}
