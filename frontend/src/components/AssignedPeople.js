import React, { useRef, useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
// import MaterialTable, { MTableToolbar } from 'material-table';
import TablePagination from '@material-ui/core/TablePagination';
import Button from '@material-ui/core/Button';
import Paper from '@material-ui/core/Paper';
import Autocomplete from '@material-ui/lab/Autocomplete';
import { TextField } from '@material-ui/core';
import * as yup from 'yup';
import PropTypes from 'prop-types';
import { api } from '../axiosInstance';

const useStyles = makeStyles(() => ({
  addButton: {
    margin: '8px 20px 8px 20px',
  },
  outerTd: {
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: 0,
  },
  tablePagination: {
    border: 0,
  },
  rowEditComponent: {
    fontSize: 13,
  },
}));

const userValidation = yup.object().required('Please select a user');
function AssignedPeople(props) {
  const { data, onChange } = props;
  const classes = useStyles();
  const [error, setError] = useState({ helperText: '', hasError: false });
  const [users, setUsers] = useState([]);
  const resetErrors = () => {
    setError({ hasError: false, helperText: '' });
  };

  useEffect(() => {
    const fetchData = async () => {
      const result = await api.get('Users');
      setUsers(result);
    };
    fetchData();
  }, []);

  const columns = [
    {
      title: 'User',
      field: 'user',
      editComponent: function editComponent(props) {
        return (
          <>
            <Autocomplete
              clearOnEscape
              options={users}
              // eslint-disable-next-line react/prop-types
              value={props.value || null}
              onInputChange={() => {
                error.hasError = false;
              }}
              onChange={(event, value) => {
                props.onChange(value);
              }}
              getOptionLabel={(option) => option.name + ', ' + option.email}
              renderInput={(params) => (
                <TextField
                  {...params}
                  error={error.hasError}
                  label={error.hasError ? error.helperText : 'Select User'}
                />
              )}
            />
          </>
        );
      },
      render: (rowData) => rowData.name + ', ' + rowData.email,
    },
  ];

  const pageSize = props.pageSize || 3;
  const addButton = useRef();
  const addButtonClick = () => {
    resetErrors();
    addButton.current.parentNode.parentNode.click();
  };

  return (
    <MaterialTable
      columns={columns}
      data={data}
      options={{
        search: false,
        pageSize: pageSize,
        pageSizeOptions: [],
        showTitle: false,
        toolbar: true,
        headerStyle: { backgroundColor: '#f1f1f1' },
        actionsColumnIndex: -1,
      }}
      components={{
        /*--- Overriding TablePagination so that the AddRow button is at the bottom of the table ---*/
        Pagination: function Pagination(props) {
          return (
            <td className={classes.outerTd}>
              <Button variant="outlined" className={classes.addButton} onClick={addButtonClick}>
                Add
              </Button>{' '}
              {/* https://github.com/mbrn/material-table/issues/830 */}
              <table>
                <tbody>
                  <tr>
                    <TablePagination className={classes.tablePagination} {...props} />
                  </tr>
                </tbody>
              </table>
            </td>
          );
        },
        /*--- Hiding the toolbar (it must exist for the custom AddRow button to work, but should be hidden for the desired design (https://github.com/mbrn/material-table/issues/830)) ---*/
        Toolbar: function Toolbar(props) {
          return (
            <div style={{ display: 'none' }}>
              <MTableToolbar {...props} />
            </div>
          );
        },

        /*--- Custom container for the table ---*/
        Container: function Container(props) {
          // eslint-disable-next-line react/prop-types
          return <Paper variant="outlined">{props.children}</Paper>;
        },
      }}
      /*--- Overriding the AddRow button icon and adding a ref to it (for the custom AddRow button to work) ---*/
      icons={{
        // eslint-disable-next-line react/display-name
        Add: () => <div ref={addButton}></div>,
      }}
      /*--- Functions for editing the table ---*/
      editable={{
        onRowAdd: async (newData) => {
          try {
            await userValidation.validate(newData.user);
            onChange([...props.data, newData.user]);
          } catch (err) {
            setError({ helperText: err.message, hasError: true });
            this.onRowAdd(newData);
          }
        },
        onRowDelete: async (oldData) => {
          try {
            const elementIndex = props.data.indexOf(oldData);
            onChange([...props.data.slice(0, elementIndex), ...props.data.slice(elementIndex + 1)]);
          } catch (err) {
            console.log(err);
          }
        },
      }}
    />
  );
}

AssignedPeople.propTypes = {
  pageSize: PropTypes.number,
  data: PropTypes.array,
  onChange: PropTypes.func.isRequired,
};

AssignedPeople.defaultProps = {
  categorySelectOptions: [],
  data: [],
  pageSize: 3,
};

export default AssignedPeople;
