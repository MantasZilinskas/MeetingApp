import React from 'react';
import { CKEditor } from '@ckeditor/ckeditor5-react';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { Box, makeStyles } from '@material-ui/core';
import { debounce } from 'lodash';
const useStyles = makeStyles((theme) => ({
  editor: { maxHeight: '600px', overflow: 'auto' },
}));

export default function TextEditor() {
  const classes = useStyles();
  const onChange = debounce((event, editor) => {
    const data = editor.getData();
    console.log(data);
  }, 5000);
  return (
    <>
      <Box className={classes.editor}>
        <CKEditor
          config={{
            toolbar: [
              'heading',
              '|',
              'FontSize',
              'bold',
              'italic',
              'link',
              'bulletedList',
              'numberedList',
              'blockQuote',
              'insertTable',
              'undo',
              'redo',
            ],
          }}
          editor={ClassicEditor}
          data="<p>Hello from CKEditor 5!</p>"
          onReady={(editor) => {
            // You can store the "editor" and use when it is needed.
            //console.log('Editor is ready to use!', editor);
          }}
          onChange={onChange}
          onBlur={(event, editor) => {}}
          onFocus={(event, editor) => {}}
        />
      </Box>
    </>
  );
}
