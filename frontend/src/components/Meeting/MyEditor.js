import React, { useState, useEffect, useRef } from 'react';
import { debounce } from 'lodash';
import { api } from '../../axiosInstance';
import { useParams } from 'react-router-dom';

export default function MyEditor({ editorData }) {
  const editorRef = useRef();
  const [editorLoaded, setEditorLoaded] = useState(false);
  const { CKEditor, ClassicEditor } = editorRef.current || {};
  const { meetingId } = useParams();
  const onChange = debounce(async (event, editor) => {
    const data = editor.getData();
    const requestData = {textEditorData: data}
    await api.put(`meeting/${meetingId}/texteditor`,requestData);
    console.log(data);
  }, 5000);

  useEffect(() => {
    const { CKEditor } = require('@ckeditor/ckeditor5-react');
    const ClassicEditor = require('@ckeditor/ckeditor5-build-classic');
    editorRef.current = {
      CKEditor: CKEditor,
      ClassicEditor: ClassicEditor,
    };
    setEditorLoaded(true);
  }, []);

  return editorLoaded ? (
    <CKEditor
      editor={ClassicEditor}
      config={{
        toolbar: [
          'heading',
          '|',
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
      data={editorData}
      onInit={(editor) => {}}
      onChange={onChange}
    />
  ) : (
    <div>Editor loading</div>
  );
}
